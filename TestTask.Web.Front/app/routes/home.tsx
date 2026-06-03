import { useEffect, useState } from "react";
import type * as React from "react";
import type { Route } from "./+types/home";
import {
  createCandidate,
  getAnalytics,
  getCandidates,
  getSession,
  login,
  logout,
  type Analytics,
  type Candidate,
  type CandidateAddRrequest,
} from "../api";
import { Alert, Button, Field, FormPanel, Input, MetricCard, Panel, Table, Td, TextLink, Th } from "../components/ui";

export function meta({ }: Route.MetaArgs) {
  return [
    { title: "Кандидаты" }
  ];
}

const emptyCandidate: CandidateAddRrequest = {
  fullName: "",
  birthDate: "",
  desiredSalary: 0,
  email: "",
  position: "",
  experienceYears: 0,
};

export default function Home() {
  const [loginName, setLoginName] = useState("");
  const [password, setPassword] = useState("");
  const [currentUser, setCurrentUser] = useState<string | null>(null);
  const [candidates, setCandidates] = useState<Candidate[]>([]);
  const [analytics, setAnalytics] = useState<Analytics | null>(null);
  const [activeSection, setActiveSection] = useState<"candidates" | "analytics">("candidates");
  const [candidateDraft, setCandidateDraft] = useState(emptyCandidate);
  const [isCandidateFormOpen, setCandidateFormOpen] = useState(false);
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  async function refreshDashboard() {
    const [candidateList, analyticsResult] = await Promise.all([getCandidates(), getAnalytics()]);
    setCandidates(candidateList);
    setAnalytics(analyticsResult);
  }

  useEffect(() => {
    void getSession()
      .then(async (session) => {
        if (session.isAuthenticated) {
          setCurrentUser(session.login);
          await refreshDashboard();
        }
      })
      .catch((reason: Error) => setError(reason.message))
      .finally(() => setLoading(false));
  }, []);

  async function onLogin(event: React.SyntheticEvent<HTMLFormElement, SubmitEvent>) {
    event.preventDefault();

    setError(null);

    try {
      const session = await login(loginName, password);

      setCurrentUser(session.login);
      setPassword("");

      await refreshDashboard();
    } catch (reason) {
      setError(getErrorMessage(reason));
    }
  }

  async function onLogout() {
    setError(null);

    try {
      await logout();

      setCurrentUser(null);
      setCandidates([]);
      setAnalytics(null);
    } catch (reason) {
      setError(getErrorMessage(reason));
    }
  }

  async function handleCandidateCreate(event: React.SyntheticEvent<HTMLFormElement, SubmitEvent>) {
    event.preventDefault();

    setError(null);

    try {
      await createCandidate(candidateDraft);

      setCandidateDraft(emptyCandidate);
      setCandidateFormOpen(false);

      await refreshDashboard();
    } catch (reason) {
      setError(getErrorMessage(reason));
    }
  }

  if (isLoading) {
    return <main className="p-4">Загрузка...</main>;
  }

  if (!currentUser) {
    return (
      <main className="p-4">
        <FormPanel onSubmit={onLogin}>
          {error && <Alert>{error}</Alert>}
          <Field label="Логин">
            <Input required value={loginName} onChange={(event) => setLoginName(event.target.value)} />
          </Field>
          <Field label="Пароль">
            <Input required type="password" value={password} onChange={(event) => setPassword(event.target.value)} />
          </Field>
          <Button type="submit">Войти</Button>
        </FormPanel>
      </main>
    );
  }

  return (
    <main className="p-4">
      <div>
        <strong>{currentUser}</strong>
        <Button onClick={() => onLogout()}>Выйти</Button>
      </div>
      <div className="grid gap-4 border-b pb-4">
        <nav className="grid gap-2">
          <Button active={activeSection === "candidates"} onClick={() => setActiveSection("candidates")}>
            <span>Кандидаты</span>
          </Button>
          <Button active={activeSection === "analytics"} onClick={() => setActiveSection("analytics")}>
            <span>Аналитика</span>
          </Button>
        </nav>
      </div>

      <section className="py-4">
        {error && <Alert>{error}</Alert>}
        {activeSection === "candidates" ? (
          <CandidatesSection
            analytics={analytics}
            candidates={candidates}
            candidateDraft={candidateDraft}
            isCandidateFormOpen={isCandidateFormOpen}
            onCancel={() => setCandidateFormOpen(false)}
            onChange={setCandidateDraft}
            onCreate={() => setCandidateFormOpen(true)}
            onSubmit={handleCandidateCreate}
          />
        ) : (
          <AnalyticsSection analytics={analytics} />
        )}
      </section>
    </main>
  );
}

function CandidatesSection({
  analytics,
  candidates,
  candidateDraft,
  isCandidateFormOpen,
  onCancel,
  onChange,
  onCreate,
  onSubmit,
}: {
  analytics: Analytics | null;
  candidates: Candidate[];
  candidateDraft: CandidateAddRrequest;
  isCandidateFormOpen: boolean;
  onCancel: () => void;
  onChange: (candidate: CandidateAddRrequest) => void;
  onCreate: () => void;
  onSubmit: (event: React.SyntheticEvent<HTMLFormElement, SubmitEvent>) => void;
}) {
  return (
    <>
      <Button type="button" onClick={onCreate}>Добавить кандидата</Button>
      {isCandidateFormOpen && (
        <CandidateForm
          candidate={candidateDraft}
          onCancel={onCancel}
          onChange={onChange}
          onSubmit={onSubmit}
        />
      )}
      <Metrics analytics={analytics} />
      <Panel>
        <div className="overflow-x-auto">
          <CandidateTable candidates={candidates} />
        </div>
      </Panel>
    </>
  );
}

function AnalyticsSection({ analytics }: { analytics: Analytics | null }) {
  return (
    <>
      <Metrics analytics={analytics} />

      <Panel>
        <div className="overflow-x-auto">
          <Table>
            <thead>
              <tr>
                <Th>Кандидат</Th>
                <Th>Должность</Th>
                <Th>Опыт</Th>
                <Th>Ожидания</Th>
                <Th>Рейтинг</Th>
              </tr>
            </thead>
            <tbody>{analytics?.ratings.map(({ candidate, score }) =>
              <tr key={candidate.id}>
                <Td>
                  <b className="block">{candidate.fullName}</b>
                  <span className="block text-sm text-gray-600">{candidate.age} лет</span>
                </Td>
                <Td>{candidate.position}</Td>
                <Td>{candidate.experienceYears} лет</Td>
                <Td>{candidate.desiredSalary}</Td>
                <Td><b>{score}</b></Td>
              </tr>)}
            </tbody>
          </Table>
        </div>
      </Panel>
    </>
  );
}

function Metrics({ analytics }: { analytics: Analytics | null }) {
  return <section className="grid gap-3 py-4">
    <MetricCard label="Всего профилей" value={analytics?.candidateCount} />
    <MetricCard label="Средняя зарплата" value={analytics?.averageSalary} />
    <MetricCard label="Средний стаж" value={analytics?.averageExperience} />
  </section>;
}

function CandidateTable({ candidates }: { candidates: Candidate[] }) {
  return <Table>
    <thead>
      <tr>
        <Th>Кандидат</Th>
        <Th>Должность</Th>
        <Th>Опыт</Th>
        <Th>Ожидания</Th>
        <Th>Контакты</Th>
      </tr>
    </thead>
    <tbody>{candidates.map((candidate) =>
      <tr key={candidate.id}>
        <Td>
          <b className="block">{candidate.fullName}</b><span className="block text-sm text-gray-600">{candidate.age} лет</span>
        </Td>
        <Td>{candidate.position}</Td>
        <Td>{candidate.experienceYears} лет</Td>
        <Td>{candidate.desiredSalary}</Td>
        <Td><TextLink href={`mailto:${candidate.email}`}>{candidate.email}</TextLink></Td>
      </tr>)}
    </tbody>
  </Table>;
}

function CandidateForm({ candidate, onCancel, onChange, onSubmit }: { candidate: CandidateAddRrequest; onCancel: () => void; onChange: (candidate: CandidateAddRrequest) => void; onSubmit: (event: React.SyntheticEvent<HTMLFormElement, SubmitEvent>) => void }) {
  return <div className="py-4">
    <FormPanel onSubmit={onSubmit}>
      <Field label="ФИО">
        <Input required maxLength={150} value={candidate.fullName} onChange={(event) => onChange({ ...candidate, fullName: event.target.value })} />
      </Field>

      <Field label="Дата рождения">
        <Input required type="date" value={candidate.birthDate} onChange={(event) => onChange({ ...candidate, birthDate: event.target.value })} />
      </Field>

      <Field label="Email">
        <Input required type="email" maxLength={150} value={candidate.email} onChange={(event) => onChange({ ...candidate, email: event.target.value })} />
      </Field>

      <Field label="Желаемая должность">
        <Input required maxLength={100} value={candidate.position} onChange={(event) => onChange({ ...candidate, position: event.target.value })} />
      </Field>

      <Field label="Желаемая зарплата">
        <Input required min={1} max={1000000} type="number" value={candidate.desiredSalary || ""} onChange={(event) => onChange({ ...candidate, desiredSalary: Number(event.target.value) })} />
      </Field>

      <Field label="Опыт работы, лет">
        <Input required min={0} max={60} type="number" value={candidate.experienceYears} onChange={(event) => onChange({ ...candidate, experienceYears: Number(event.target.value) })} />
      </Field>

      <div className="flex gap-3">
        <Button type="button" onClick={onCancel}>Отмена</Button>
        <Button type="submit">Добавить</Button>
      </div>
    </FormPanel>
  </div>;
}

function getErrorMessage(reason: any) {
  return reason instanceof Error ? reason.message : "Ошибка отправки запроса";
}

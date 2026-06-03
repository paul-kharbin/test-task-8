export interface Session {
  isAuthenticated: boolean;
  login: string | null;
}

export interface Candidate {
  id: number;
  fullName: string;
  birthDate: string;
  age: number;
  desiredSalary: number;
  email: string;
  position: string;
  experienceYears: number;
  createdAtUtc: string;
}

export interface CandidateAddRrequest {
  fullName: string;
  birthDate: string;
  desiredSalary: number;
  email: string;
  position: string;
  experienceYears: number;
}

export interface Analytics {
  candidateCount: number;
  averageSalary: number;
  averageExperience: number;
  ratings: Array<{ candidate: Candidate; score: number }>;
}

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(path, {
    ...init,
    headers: {
      "Content-Type": "application/json",
      ...(init?.headers ?? {}),
    },
    credentials: "include",
  });

  const text = await response.text();
  if (!response.ok) {
    const problem = text ? JSON.parse(text) : null;
    const message = problem?.detail ?? problem?.title ?? "Ошибка отправки запоса";

    alert(message);

    throw new Error(message);
  }

  if (!text) {
    return undefined as T;
  }

  return JSON.parse(text) as T;
}

export function getSession() {
  return request<Session>("/api/auth");
}

export function login(login: string, password: string) {
  return request<Session>("/api/auth/login", {
    method: "POST",
    body: JSON.stringify({ login, password }),
  });
}

export function logout() {
  return request<void>("/api/auth/logout", { method: "DELETE" });
}

export function getCandidates() {
  return request<Candidate[]>("/api/candidates");
}

export function createCandidate(candidate: CandidateAddRrequest) {
  return request<Candidate>("/api/candidates", {
    method: "POST",
    body: JSON.stringify(candidate),
  });
}

export function getAnalytics() {
  return request<Analytics>("/api/candidates/analytics");
}

import type * as React from "react";

type ButtonProps = React.ButtonHTMLAttributes<HTMLButtonElement> & {
  active?: boolean;
};

export function Button({ active = false, className = "", ...props }: ButtonProps) {
  return <button className={`border p-2 ${active ? "font-bold" : ""} ${className}`} {...props} />;
}

export function Input(props: React.InputHTMLAttributes<HTMLInputElement>) {
  return <input className="border p-2" {...props} />;
}

export function Field({ children, label }: { children: React.ReactNode; label: string }) {
  return (
    <label className="grid gap-1">
      {label}
      {children}
    </label>
  );
}

export function Panel({ children, className = "" }: { children: React.ReactNode; className?: string }) {
  return <section className={`border p-4 ${className}`}>{children}</section>;
}

export function FormPanel({ children, onSubmit }: { children: React.ReactNode; onSubmit: React.FormEventHandler<HTMLFormElement> }) {
  return (
    <form className="grid gap-3 border p-4" onSubmit={onSubmit}>
      {children}
    </form>
  );
}

export function Alert({ children }: { children: React.ReactNode }) {
  return <p className="text-red-700">{children}</p>;
}

export function MetricCard({ label, value }: { label: string; value: React.ReactNode }) {
  return (
    <article className="border p-3">
      <p>{label}</p>
      <b>{value}</b>
    </article>
  );
}

export function Table({ children }: { children: React.ReactNode }) {
  return <table className="w-full">{children}</table>;
}

export function Th({ children }: { children: React.ReactNode }) {
  return <th className="border p-2 text-left">{children}</th>;
}

export function Td({ children }: { children: React.ReactNode }) {
  return <td className="border p-2">{children}</td>;
}

export function TextLink({ children, href }: { children: React.ReactNode; href: string }) {
  return <a href={href}>{children}</a>;
}

import Link from "next/link";

export default function Home() {
  return (
    <header className="border-b border-slate-200 bg-white">
      <div className="mx-auto flex w-full max-w-6xl items-center justify-between px-6 py-5">
        <Link className="text-xl font-bold text-slate-900" href="/">
          Kioskitos
        </Link>
        <Link
          className="rounded-md bg-blue-700 px-4 py-2 text-sm font-semibold text-white hover:bg-blue-800"
          href="/login"
        >
          Iniciar sesión
        </Link>
      </div>
    </header>
  );
}

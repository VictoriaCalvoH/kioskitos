const configuredApiUrl = process.env.NEXT_PUBLIC_API_URL;

if (!configuredApiUrl) {
  throw new Error("The NEXT_PUBLIC_API_URL variable is not configured.");
}

const API_URL = configuredApiUrl;

export async function apiFetch<T>(
  path: string,
  options?: RequestInit,
): Promise<T> {
  const response = await fetch(
    `${API_URL.replace(/\/$/, "")}/${path.replace(/^\//, "")}`,
    {
      ...options,
      headers: {
        "Content-Type": "application/json",
        ...options?.headers,
      },
    },
  );

  if (!response.ok) {
    throw new Error(
      `The API responded with the HTTP status ${response.status}.`,
    );
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return (await response.json()) as T;
}

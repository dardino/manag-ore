export type FetchFn = typeof fetch;

export class ApiFetchAdapter {
  constructor(private getAccessToken: () => Promise<string | null>, private fetchImpl: FetchFn = fetch) {}

  async fetchWithAuth(input: RequestInfo, init?: RequestInit) {
    const token = await this.getAccessToken();
    const headers = new Headers(init?.headers ?? {});
    if (token) headers.set('Authorization', `Bearer ${token}`);
    const merged: RequestInit = { ...init, headers };
    return this.fetchImpl(input, merged);
  }
}

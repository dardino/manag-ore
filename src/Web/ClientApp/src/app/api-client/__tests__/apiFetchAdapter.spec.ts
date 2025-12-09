import { describe, it, expect, vi } from 'vitest';
import { ApiFetchAdapter } from '../apiFetchAdapter';

describe('ApiFetchAdapter', () => {
  it('attaches Authorization header when token present', async () => {
    const token = 'fake-token';
    const fetchMock = vi.fn().mockResolvedValue({ ok: true, json: async () => ({}) });
    const adapter = new ApiFetchAdapter(() => Promise.resolve(token), fetchMock as any);

    await adapter.fetchWithAuth('/test', { method: 'GET' });

    expect(fetchMock).toHaveBeenCalled();
    const [[, init]] = fetchMock.mock.calls;
    const header = init.headers.get('Authorization');
    expect(header).toBe(`Bearer ${token}`);
  });
});

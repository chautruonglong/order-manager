import jwtDecode from 'jwt-decode'

export const TOKENS = {
  ACCESS: 'access-token',
  REFRESH: 'refresh-token',
}

export const setAccessToken = (token: string): void => localStorage.setItem(TOKENS.ACCESS, token)
export const setRefreshToken = (token: string): void => localStorage.setItem(TOKENS.REFRESH, token)

export const getAccessToken = (): string | null => localStorage.getItem(TOKENS.ACCESS)
export const getRefreshToken = (): string | null => localStorage.getItem(TOKENS.REFRESH)

export const clearTokens = (): void => {
  localStorage.removeItem(TOKENS.ACCESS)
  localStorage.removeItem(TOKENS.REFRESH)
}

export const isValidToken = (token: string): boolean => {
  const jwt = jwtDecode<any>(token)
  console.log(jwt)
  return true
}

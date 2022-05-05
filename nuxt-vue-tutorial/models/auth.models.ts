export interface Login {
  email: string
  password: string
}

export interface AccessToken {
  accessToken: string
  refreshToken: string
}

export interface UserRegister {
  email: string
  name: string
  password: string
}

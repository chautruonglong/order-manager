import { Context } from '@nuxt/types'
import { getAccessToken, isValidToken, setAccessToken, setRefreshToken } from '@utils'
import { AUTH_MUTATIONS } from '@store/auth'

export default async ({ store, route, redirect }: Context) => {
  const accessToken = getAccessToken()
  const currentPath = route.path

  if (accessToken && isValidToken(accessToken)) {
    store.commit(AUTH_MUTATIONS.MUTATE_IS_AUTHENTICATED, true, { root: true })

    if (currentPath == '/login') {
      redirect('/')
    }
  } else {
    try {
      const accessTokens = await store.$api.auth.refresh()

      if (accessTokens) {
        if (accessTokens.accessToken) {
          setAccessToken(accessTokens.accessToken)
          store.commit(AUTH_MUTATIONS.MUTATE_IS_AUTHENTICATED, true, { root: true })
        }
        if (accessTokens.refreshToken) {
          setRefreshToken(accessTokens.refreshToken)
        }
      }
    } catch {
      if (currentPath !== '/' && currentPath !== '/login') {
        redirect(`/login?redirectLink=${currentPath}`)
      }
      store.commit(AUTH_MUTATIONS.MUTATE_IS_AUTHENTICATED, false, { root: true })
    }
  }
}

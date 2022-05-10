export const COMMON_NAMESPACED = 'common'

export interface Snackbar {
  active: boolean
  message: string
  timeout: number
}

export interface CommonState {
  isLoading: boolean
  isShowProductCreation: boolean
  isShowProductModification: boolean
  isShowProductModal: boolean
  isShowCart: boolean
  snackbar: Snackbar
}

const commonState: CommonState = {
  isLoading: true,
  isShowProductCreation: false,
  isShowProductModification: false,
  isShowProductModal: false,
  isShowCart: false,
  snackbar: {
    active: false,
    message: '',
    timeout: 3,
  },
}

export default () => commonState

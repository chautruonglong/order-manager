import { AuthService } from '@services/auth.service'
import { BillService } from '@services/bill.service'
import { UserService } from '@services/user.service'
import { ProductService } from '@services/product.service'

export interface AppService {
  auth: AuthService
  bill: BillService
  user: UserService
  product: ProductService
}

export { AuthService }

export * from '@services/auth.service'
export * from '@services/bill.service'
export * from '@services/user.service'
export * from '@services/product.service'

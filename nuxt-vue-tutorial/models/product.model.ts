export interface Product {
  id: string
  name: string
  image: string
  description: string
  price: number
  discount: number
  createdAt: Date
  modifiedAt: Date
}

export interface ProductCreation {
  name: string
  image: string
  description: string
  price: number
  discount: number
}

export interface ProductModification {
  name: string
  image: string
  description: string
  price: number
  discount: number
}

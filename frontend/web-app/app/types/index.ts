export type PagedResult<T> = {
    results: T[]
    pageCount: number
    totalCount: number
}

export type Auction = {
    id: string
    reservePrice: number
    seller: string
    winner?: string
    myProperty: number
    currentHighBid: number
    createdAt: string
    updatedAt: string
    auctionEnd: string
    status: string
    make: string
    model: string
    year: number
    color: string
    mileage: number
    imageUrl: string
}

export type Bid = {
    id: string
    auctionId: string
    bidder: string
    bidTime: string
    amount: number
    bidStatus: string
}

export type AuctionFinished = {
    itemSold: boolean
    auctionId: string
    winner?: string
    seller: string
    amount?: number
}
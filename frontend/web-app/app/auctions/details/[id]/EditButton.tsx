'use client'

import { Button } from 'flowbite-react'
import Link from 'next/link'
import React from 'react'

type Props = {
    id: string
}

export default function EditButton({id}: Props) {
  return (
    <Button className='bg-red-400 enabled:hover:bg-red-500 text-white' outline>
        <Link href={`/auctions/update/${id}`}>Update Auction</Link>
    </Button>
  )
}

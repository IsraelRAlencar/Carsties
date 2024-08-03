'use client'

import { Button } from 'flowbite-react'
import Link from 'next/link'
import React from 'react'

export default function UserActions() {
    return (
        <Button outline className='bg-red-400 enabled:hover:bg-red-500 text-white'>
            <Link href='/session'>
                Session
            </Link>
        </Button>
    )
}

'use client'

import { Button } from 'flowbite-react'
import { signIn } from 'next-auth/react'
import React from 'react'

export default function LoginButton() {
  return (
    <Button className='enabled:hover:bg-red-100 enabled:hover:text-red-500 focus:ring-0 bg-red-400' 
        outline onClick={() => signIn('id-server', {callbackUrl: '/'})}>
        Login
    </Button>
  )
}

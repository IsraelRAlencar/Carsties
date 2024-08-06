'use client'

import { Dropdown, DropdownDivider } from 'flowbite-react'
import { User } from 'next-auth'
import { signOut } from 'next-auth/react'
import Link from 'next/link'
import React from 'react'
import { AiFillCar, AiFillTrophy, AiOutlineLogout } from 'react-icons/ai'
import { HiCog, HiUser } from 'react-icons/hi2'

type Props = {
    user: Partial<User>
}

export default function UserActions({user}: Props) {
    return (
        <Dropdown className='z-50' color={'red'} label={`Welcome ${user.name}`} dismissOnClick={false}>
            <Dropdown.Item icon={HiUser}><Link href='/'>My Auctions</Link></Dropdown.Item>
            <Dropdown.Item icon={AiFillTrophy}><Link href='/'>Auctions Won</Link></Dropdown.Item>
            <Dropdown.Item icon={AiFillCar}><Link href='/auctions/create'>Sell my Car</Link></Dropdown.Item>
            <Dropdown.Item icon={HiCog}><Link href='/session'>Session (dev only)</Link></Dropdown.Item>
            <DropdownDivider />
            <Dropdown.Item icon={AiOutlineLogout} onClick={() => signOut({callbackUrl: '/'})} >Sign Out</Dropdown.Item>
        </Dropdown>
    )
}

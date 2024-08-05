'use client'

import React, { useState } from 'react'
import { UpdateAuctionTest } from '../actions/AuctionActions';
import { Button } from 'flowbite-react';

export default function AuthTest() {
    const [loading, setLoading] = useState(false);
    const [result, setResult] = useState<any>();

    function doUpdate() {
        setResult(undefined);
        setLoading(true);
        UpdateAuctionTest().then(res => setResult(res)).finally(() => setLoading(false))
    }

    return (
        <div className="flex items-center gap-4">
            <Button  className='bg-red-400 enabled:hover:bg-red-500 text-white' outline isProcessing={loading} onClick={doUpdate}>
                Test auth
            </Button>
            <div>
                {JSON.stringify(result, null, 2)}
            </div>
        </div>
    )
}

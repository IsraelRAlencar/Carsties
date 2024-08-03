
import React from 'react'
import Heading from '../components/Heading';
import { getSession } from '../actions/authActions';

export default async function Session() {
    const session = await getSession();

    return (
        <div>
            <Heading title='Session dashboard' subtitle={''} />

            <div className="bg-red-200 border-2 border-red-500">
                <h3 className='text-lg' >Session data</h3>
                <pre>{JSON.stringify(session, null, 2)}</pre>
            </div>
        </div>
    )
}

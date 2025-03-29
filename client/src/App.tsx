import React from 'react'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"

const queryClient = new QueryClient()

import Home from './routes/Home'
import SignUp from './routes/SignUp'
import SignIn from './routes/SignIn'

function App() {
    const BrowserRouter = createBrowserRouter([
        { path: '/', element: <Home /> },
        { path: '/signup', element: <SignUp /> },
        { path: '/login', element: <SignIn /> },
    ])

    return (
        <QueryClientProvider client={queryClient}>
            <RouterProvider router={BrowserRouter} />
        </QueryClientProvider>
    )
}

export default App

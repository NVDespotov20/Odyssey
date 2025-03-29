import React from 'react'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import { Toaster } from "@/components/ui/sonner"

const queryClient = new QueryClient()

import Protected from './components/Protected'

import Home from './routes/Home'
import SignUp from './routes/SignUp'
import SignIn from './routes/SignIn'
import Browse from './routes/Browse'

function App() {
    const BrowserRouter = createBrowserRouter([
        { path: '/', element: <Home /> },
        { path: '/signup', element: <SignUp /> },
        { path: '/login', element: <SignIn /> },
        { path: '/browse', element: <Protected><Browse /> </Protected> }
    ])

    return (
        <QueryClientProvider client={queryClient}>
            <RouterProvider router={BrowserRouter} />
            <Toaster />
        </QueryClientProvider>
    )
}

export default App

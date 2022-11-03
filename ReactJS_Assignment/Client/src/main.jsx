import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import { ChakraProvider } from '@chakra-ui/react'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import { ErrorPage, HomePage, LogInPage, RookiesPage } from './pages'
import { rookieDeleteAction, rookieDetailsLoader, RookieDetailsPage, RookiesList, rookiesLoader } from './pages/Rookies/subpages'

const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      {
        index: true,
        element: <HomePage />
      },
      {
        path: '/rookies',
        element: <RookiesPage />,
        children: [
          {
            index: true,
            element: <RookiesList />,
            loader: rookiesLoader
          },
          {
            path:'/rookies/:rookieId',
            element: <RookieDetailsPage />,
            loader: rookieDetailsLoader
          },
          {
            path: '/rookies/:rookieId/delete',
            action: rookieDeleteAction
          }
        ]
      },
      {
        path: '/login',
        element: <LogInPage />
      }
    ]
  }
]);

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <ChakraProvider>
      <RouterProvider router={router} />
    </ChakraProvider>
  </React.StrictMode>
)

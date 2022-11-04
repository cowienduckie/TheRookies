import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import { ChakraProvider } from '@chakra-ui/react'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import { ErrorPage, HomePage, LoginAction, LoginLoader, LogInPage, ProfileLoader, ProfilePage, RookiesPage } from './pages'
import { rookieCreateAction, rookieCreateLoader, RookieCreatePage, rookieDeleteAction, rookieDetailsLoader, RookieDetailsPage, RookiesList, rookiesLoader, rookieUpdateAction, RookieUpdatePage } from './pages/Rookies/subpages'

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
            path: '/rookies/new',
            element: <RookieCreatePage />,
            action: rookieCreateAction,
            loader: rookieCreateLoader
          },
          {
            path: '/rookies/:rookieId/edit',
            element: <RookieUpdatePage />,
            action: rookieUpdateAction,
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
        element: <LogInPage />,
        action: LoginAction,
        loader: LoginLoader
      },
      {
        path: '/profile',
        element: <ProfilePage />,
        loader: ProfileLoader
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

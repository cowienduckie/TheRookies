import { Container } from '@chakra-ui/react';
import { useState } from 'react';
import { Outlet } from 'react-router-dom';
import { Header } from './components';
import { TOKEN_KEY } from './constants/system-constants';
import { authContext } from './contexts/auth-context'

function App() {
  const [authenticated, setAuthenticated] = useState(localStorage.getItem(TOKEN_KEY) != null);

  return (
    <authContext.Provider value={{ authenticated, setAuthenticated }}>
      <Header />
      <Container className='Body' maxW="container.xl" p={0}>
        <Outlet />
      </Container>
    </authContext.Provider>
  )
}

export default App;
import { Container } from '@chakra-ui/react';
import { Outlet } from 'react-router-dom';
import { Header } from './components';

function App() {
    return <>
      <Header />
      <Container className='Body' maxW="container.xl" p={0}>
        <Outlet />
      </Container>
    </>
  }

export default App;
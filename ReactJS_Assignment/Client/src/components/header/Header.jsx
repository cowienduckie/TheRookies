import { Flex, Link, Heading, Button, Spacer, LinkBox } from '@chakra-ui/react';
import { useContext } from 'react';
import { NavLink } from 'react-router-dom';
import { TOKEN_KEY } from '../../constants/system-constants';
import { authContext } from '../../contexts/auth-context';

export function Header() {
  const { authenticated, setAuthenticated } = useContext(authContext);

  const onLogOut = () => {
    localStorage.removeItem(TOKEN_KEY);
    setAuthenticated(false);
  }

  return <>
    <Flex
      minW='max-content'
      py={4}
      px={20}
      alignItems='center'
      gap='6'
      bg="gray.100"
    >
      <Heading size="md" >
        Rookies App
      </Heading>
      <Link as={NavLink} to='/'>Home</Link>
      <Link as={NavLink} to='/rookies'>Rookies</Link>
      <Spacer />
      {!authenticated ?
        (
          <LinkBox as={NavLink} to='/login'>
            <Button colorScheme='blue'>Login</Button>
          </LinkBox>
        ) : <>
          <Link as={NavLink} to='/profile'>Profile</Link>
          <Link as={NavLink} to='/' onClick={onLogOut}>Logout</Link>
        </>}
    </Flex>
  </>
}
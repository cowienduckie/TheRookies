import { Flex, Link, Heading, Button, Spacer } from '@chakra-ui/react';
import { NavLink } from 'react-router-dom';

export function Header() {
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
      <Button>Log In</Button>
    </Flex>
  </>
}
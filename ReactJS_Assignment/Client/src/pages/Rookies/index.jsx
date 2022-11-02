import { Box, Heading, Divider } from '@chakra-ui/react';
import { Outlet } from 'react-router-dom';

export function RookiesPage() {
  return <>
    <Box p={10}>
      <Heading size='xl'>ROOKIES PAGE</Heading>
      <Divider my={7} />
      <Outlet />
    </Box>
  </>
}
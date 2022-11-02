import { Container, Flex, VStack } from '@chakra-ui/react';
import { Fragment } from 'react';

function App() {
  return (
    <Fragment>
      <Flex minW='max-content' alignItems='center' gap='2' w="full" bg="black">
      <Heading size='md'>Chakra App</Heading>
      </Flex>
      <Container maxW="container.xl" p={0}>
        <Flex h="100vh" py={20}>
          <VStack
            w="full"
            h="full"
            p={10}
            spacing={10}
            alignItems="flex-start">

          </VStack>
          <VStack
            w="full"
            h="full"
            p={10}
            spacing={10}
            alignItems="flex-start"
            bg="gray.50">

          </VStack>
        </Flex>
      </Container>
    </Fragment>

  )
}

export default App;
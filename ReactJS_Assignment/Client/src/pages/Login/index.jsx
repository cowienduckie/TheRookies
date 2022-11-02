import { Center, Divider, Heading, InputGroup, VStack, Input, InputRightElement, Button, Spacer } from "@chakra-ui/react";
import { useState } from "react";

export function LogInPage() {
  const [show, setShow]  = useState(false)
  const handleClick = () => setShow(!show)

  return <>
    <Center p={10}>
      <VStack w='60%' p={20} spacing={10} bgColor='gray.50' borderRadius='xl'>
        <Heading size='2xl'>LOG IN</Heading>
        <Spacer/>
        <Input size='lg' variant='flushed' placeholder='Enter email' />
        <InputGroup>
          <Input
            size='lg'
            pr='4.5rem'
            variant='flushed'
            type={show ? 'text' : 'password'}
            placeholder='Enter password'
          />
          <InputRightElement width='4.5rem'>
            <Button h='1.75rem' size='sm' onClick={handleClick}>
              {show ? 'Hide' : 'Show'}
            </Button>
          </InputRightElement>
        </InputGroup>
        <Button
            mt={4}
            colorScheme='teal'
          >
            Submit
          </Button>
      </VStack>
    </Center>
  </>
}
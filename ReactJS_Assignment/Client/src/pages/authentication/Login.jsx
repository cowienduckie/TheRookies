import { Center, Heading, InputGroup, VStack, Input, InputRightElement, Button, Spacer } from "@chakra-ui/react";
import { useContext, useState } from "react";
import { Form, redirect } from "react-router-dom";
import { login } from "../../apis/authentication";
import { TOKEN_KEY } from "../../constants/system-constants";
import { authContext } from "../../contexts/auth-context";

export async function action({request}) {
  const formData = await request.formData();
  const loginInfo = Object.fromEntries(formData);

  var response = await login(loginInfo);

  if (!response) {
    throw new Response("", {
      status: 400,
      statusText: 'Bad Request'
    })
  }
  
  return redirect(`/`);
}

export function loader() {
  if (localStorage.getItem(TOKEN_KEY) != null) {
    return redirect('/');
  }
}

export function LogInPage() {
  const [show, setShow]  = useState(false);
  const handleClick = () => setShow(!show);
  
  return <Form action='/login' method='post'>
    <Center p={10}>
      <VStack w='60%' p={20} spacing={10} bgColor='gray.50' borderRadius='xl'>
        <Heading size='2xl'>LOG IN</Heading>
        <Spacer/>
        <Input size='lg' variant='flushed' name='username' placeholder='Enter username' />
        <InputGroup>
          <Input
            size='lg'
            pr='4.5rem'
            variant='flushed'
            name='password'
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
            type='submit'
            mt={4}
            colorScheme='teal'
          >
            Submit
          </Button>
      </VStack>
    </Center>
  </Form>
}
import { Center, Heading, InputGroup, VStack, Input, InputRightElement, Button, Spacer } from "@chakra-ui/react";
import { useState } from "react";
import { Form, redirect } from "react-router-dom";
import { logIn } from "../../Apis/AuthenticationApis";
import { TOKEN_KEY } from "../../Constants/SystemConstants";

export function loader() {
  if (localStorage.getItem(TOKEN_KEY) != null) {
    return redirect("/");
  }
}

export async function action({ request }) {
  const formData = await request.formData();
  const loginInfo = Object.fromEntries(formData);

  var response = await logIn(loginInfo);

  return redirect(`/`);
}

export function AuthenticatePage() {
  const [show, setShow] = useState(false);
  const handleClick = () => setShow(!show);

  return (
    <Form action="/authenticate" method="post">
      <Center p={10}>
        <VStack w="60%" p={20} spacing={10} bgColor="gray.50" borderRadius="xl">
          <Heading size="2xl">LOG IN</Heading>
          <Spacer />
          <Input size="lg" variant="flushed" name="username" placeholder="Enter username" />
          <InputGroup>
            <Input
              size="lg"
              pr="4.5rem"
              variant="flushed"
              name="password"
              type={show ? "text" : "password"}
              placeholder="Enter password"
            />
            <InputRightElement width="4.5rem">
              <Button h="1.75rem" size="sm" onClick={handleClick}>
                {show ? "Hide" : "Show"}
              </Button>
            </InputRightElement>
          </InputGroup>
          <Button
            type="submit"
            mt={4}
            colorScheme="teal"
          >
            Submit
          </Button>
        </VStack>
      </Center>
    </Form>
  )
}
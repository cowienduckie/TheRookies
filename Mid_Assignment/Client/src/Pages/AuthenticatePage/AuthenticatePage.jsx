import {
  Center,
  Heading,
  InputGroup,
  VStack,
  Input,
  InputRightElement,
  Button,
  Spacer,
} from "@chakra-ui/react";
import { useEffect } from "react";
import { useContext, useRef } from "react";
import { useState, useCallback } from "react";
import { Form, redirect, useNavigate } from "react-router-dom";
import { logIn } from "../../Apis/AuthenticationApis";
import { TOKEN_KEY } from "../../Constants/SystemConstants";
import { AuthContext } from "../../Contexts/AuthContext";

export function loader() {
  if (localStorage.getItem(TOKEN_KEY) != null) {
    return redirect("/");
  }
}

export function AuthenticatePage() {
  const authContext = useContext(AuthContext);
  const navigate = useNavigate();

  const [isSending, setIsSending] = useState(false);
  const [loginInfo, setLoginInfo] = useState({
    username: "",
    password: "",
  });

  const sendRequest = (event) => {
    event.preventDefault();
    if (isSending) return;

    setIsSending(true);

    logIn(loginInfo)
      .then((userInfo) => {
        authContext.setAuthInfo(userInfo.role, userInfo.token);
        setIsSending(false);
        navigate("/");
      })
      .catch((error) => {
        setIsSending(false);
        throw new Response("", {
          status: error.status,
          statusText: error.statusText,
        });
      });
  };

  const [show, setShow] = useState(false);
  const handleClick = () => setShow(!show);

  return (
    <Form onSubmit={sendRequest}>
      <Center p={10}>
        <VStack w="60%" p={20} spacing={10} bgColor="gray.50" borderRadius="xl">
          <Heading size="2xl">LOG IN</Heading>
          <Spacer />
          <Input
            size="lg"
            variant="flushed"
            name="username"
            placeholder="Enter username"
            onChange={(e) => {
              setLoginInfo({ ...loginInfo, ["username"]: e.target.value });
            }}
          />
          <InputGroup>
            <Input
              size="lg"
              pr="4.5rem"
              variant="flushed"
              name="password"
              type={show ? "text" : "password"}
              placeholder="Enter password"
              onChange={(e) => {
                setLoginInfo({ ...loginInfo, ["password"]: e.target.value });
              }}
            />
            <InputRightElement width="4.5rem">
              <Button h="1.75rem" size="sm" onClick={handleClick}>
                {show ? "Hide" : "Show"}
              </Button>
            </InputRightElement>
          </InputGroup>
          <Button type="submit" mt={4} colorScheme="teal" disabled={isSending}>
            Submit
          </Button>
        </VStack>
      </Center>
    </Form>
  );
}

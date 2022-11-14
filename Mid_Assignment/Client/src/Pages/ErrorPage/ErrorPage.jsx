import { Center, Heading, Link, Spacer, Text, VStack } from "@chakra-ui/react";
import { NavLink, useRouteError } from "react-router-dom";

export function ErrorPage() {
  const error = useRouteError();

  return (
    <Center id="error-page" h="100vh">
      <VStack spacing={5}>
        <Heading size="3xl">Oops!</Heading>
        <Spacer />
        <Text fontSize="3xl">
          Sorry, an unexpected error has occurred.{" "}
          <Link as={NavLink} to="/" color="blue.200">
            Back to Home
          </Link>
        </Text>
        <Spacer />
        <Text fontSize="2xl">
          <strong>[{error.status}]</strong> {error.statusText || error.message}
        </Text>
      </VStack>
    </Center>
  );
}

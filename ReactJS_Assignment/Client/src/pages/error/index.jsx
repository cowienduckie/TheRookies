import { Center, Heading, Spacer, Text, VStack } from "@chakra-ui/react";
import { useRouteError } from "react-router-dom";

export function ErrorPage() {
  const error = useRouteError();
  console.error(error);

  return (
    <Center id='error-page' h='100vh'>
      <VStack spacing={5} >
        <Heading size='3xl'>Oops!</Heading>
        <Spacer/>
        <Text fontSize='3xl'>Sorry, an unexpected error has occurred.</Text>
        <Spacer/>
        <Text fontSize='2xl'>{error.statusText || error.message}</Text>
      </VStack>
    </Center>
  );
}
import { Box, Heading, Text, Divider, Image, Spacer } from "@chakra-ui/react";
import { useContext } from "react";
import { TOKEN_KEY } from "../../Constants/SystemConstants";
import { AuthContext } from "../../Contexts/AuthContext";

export function HomePage() {
  const { setAuthenticated } = useContext(AuthContext);

  setAuthenticated(localStorage.getItem(TOKEN_KEY) != null);

  return <>
    <Box p={10}>
      <Heading size="xl">HOME PAGE</Heading>
      <Divider my={7} />
      <Text
        fontSize="xl"
        align="justify"
      >
        Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry"s standard
        dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.
        It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.
        It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with
        desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
      </Text>
      <Spacer my={7} />
      <Box w="full">
        <Image src="https://dummyimage.com/16:9x1080/" alt="Dummy banner" m="auto" w="70%"/>
      </Box>
    </Box>
  </>
}
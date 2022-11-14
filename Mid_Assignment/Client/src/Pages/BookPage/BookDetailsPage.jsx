import {
  HStack,
  Image,
  VStack,
  Heading,
  Text,
  ButtonGroup,
  Button,
  Tag,
  Wrap,
} from "@chakra-ui/react";
import { useContext } from "react";
import { useLoaderData, useNavigate } from "react-router-dom";
import { getBookById } from "../../Apis/BookApis";
import { LinkButton } from "../../Components";
import { NORMAL_USER } from "../../Constants/SystemConstants";
import { AuthContext } from "../../Contexts/AuthContext";
import { BorrowRequestContext } from "../../Contexts/BorrowRequestContext";

export async function loader({ params }) {
  return await getBookById(params.bookId);
}

export function BookDetailsPage() {
  const book = useLoaderData();
  const navigate = useNavigate();
  const requestContext = useContext(BorrowRequestContext);
  const authContext = useContext(AuthContext);

  return (
    <HStack w="full" spacing={10} p={0} align="top">
      <Image
        boxSize="15%"
        src={book.cover}
        alt="Book cover"
        w={300}
        h={450}
        m="auto"
      />
      <VStack w="70%" p={10} spacing={10} bgColor="gray.50" align="left">
        <Heading size="2xl">{book.name.toUpperCase()}</Heading>
        <Text fontSize="2xl">
          <strong>ID:</strong> {book.id}
        </Text>
        <Text fontSize="2xl">
          <strong>Description:</strong> {book.description ?? "No Description"}
        </Text>
        <Text fontSize="2xl">
          <strong>Category: </strong>
        </Text>
        <Wrap mt={5}>
          {book.categories.map((c) => (
            <Tag key={c.id} size={"lg"} colorScheme={"messenger"}>
              {c.name}
            </Tag>
          ))}
        </Wrap>
        <ButtonGroup>
          {authContext.userRole === NORMAL_USER && (
            <LinkButton
              path={`/borrow-books/new`}
              text="Borrow This Book"
              label="Borrow This Book"
              mt={4}
              p={5}
              colorScheme="teal"
              onClick={() => requestContext.addBookToRequest(book.id)}
            />
          )}
          <Button
            label="Back"
            mt={4}
            p={5}
            colorScheme="red"
            onClick={() => navigate(-1)}
          >
            Back
          </Button>
        </ButtonGroup>
      </VStack>
    </HStack>
  );
}

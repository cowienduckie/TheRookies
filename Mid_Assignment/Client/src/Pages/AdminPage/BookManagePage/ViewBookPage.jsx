import {
  HStack,
  Image,
  VStack,
  Heading,
  Text,
  ButtonGroup,
  Button,
  Wrap,
  Tag,
} from "@chakra-ui/react";
import { useLoaderData } from "react-router-dom";
import { getBookById } from "../../../Apis/BookApis";
import { LinkButton } from "../../../Components";

export async function loader({ params }) {
  return await getBookById(params.bookId);
}

export function ViewBookPage() {
  const book = useLoaderData();

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
        <Text fontSize="2xl" textAlign="justify">
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
          <LinkButton
            path={`/admin/books/${book.id}/edit`}
            text="Edit This Book"
            label="Edit This Book"
            mt={4}
            p={5}
            colorScheme="teal"
          />
          <LinkButton
            path="/admin/books"
            text="Back To Book List"
            label="Back To Book List"
            mt={4}
            p={5}
            colorScheme="red"
          />
        </ButtonGroup>
      </VStack>
    </HStack>
  );
}

import {
  HStack,
  Image,
  VStack,
  Heading,
  Text,
  Wrap,
  WrapItem,
  LinkBox,
} from "@chakra-ui/react";
import { NavLink, useLoaderData } from "react-router-dom";
import { getBorrowRequestById } from "../../Apis/BorrowRequestApis";
import { LinkButton } from "../../Components";

export async function loader({ params }) {
  return await getBorrowRequestById(params.requestId);
}

export function BorrowBookDetailsPage() {
  const request = useLoaderData();

  return (
    <VStack w="full" p={10} spacing={10} bgColor="gray.50" align="left">
      <Heading size="2xl">{"BORROW REQUEST #" + request.id}</Heading>
      <HStack p={0} align="flex-end">
        <VStack w="50%" p={0} align="flex-start" spacing={10}>
          <Text fontSize="2xl">
            <strong>Status:</strong> {request.status}
          </Text>
          <Text fontSize="2xl">
            <strong>Requested At:</strong> {request.requestedAt}
          </Text>
          <Text fontSize="2xl">
            <strong>Requested By:</strong>{" "}
            {`${request.requester.name} (${request.requester.username})`}
          </Text>
        </VStack>
        {request.approvedAt && request.approver && (
          <VStack w="50%" p={0} align="flex-start" spacing={10}>
            <Text fontSize="2xl">
              <strong>Approved At:</strong> {request.approvedAt}
            </Text>
            <Text fontSize="2xl">
              <strong>Approved By:</strong>{" "}
              {`${request.approver.name} (${request.approver.username})`}
            </Text>
          </VStack>
        )}
      </HStack>
      <Text fontSize="2xl">
        <strong>{"Included  Book(s):"}</strong>
      </Text>
      <Wrap spacing={10}>
        {request.books.map((book) => (
          <WrapItem key={book.id} w={"15%"}>
            <LinkBox as={NavLink} to={`/books/${book.id}`}>
              <VStack w="full" spacing={10} p={0} align="center">
                <Image
                  src={book.cover}
                  alt="Book cover"
                  w={100}
                  h={150}
                  m="auto"
                />
                <Heading
                  size="md"
                  align="center"
                >{`#${book.id} ${book.name}`}</Heading>
              </VStack>
            </LinkBox>
          </WrapItem>
        ))}
      </Wrap>
      <LinkButton
        path="/borrow-books"
        text="Back To List"
        label="Back To List"
        mt={4}
        p={5}
        colorScheme="red"
      />
    </VStack>
  );
}

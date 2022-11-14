import {
  HStack,
  Image,
  VStack,
  Heading,
  Text,
  ButtonGroup,
  Wrap,
  WrapItem,
  LinkBox,
} from "@chakra-ui/react";
import { NavLink, useLoaderData } from "react-router-dom";
import { getBorrowRequestById } from "../../../Apis/BorrowRequestApis";
import { LinkButton } from "../../../Components";
import { FormButton } from "../../../Components/FormIconButton/FormButton";

export async function loader({ params }) {
  return await getBorrowRequestById(params.requestId);
}

export function ViewRequestPage() {
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
            <LinkBox as={NavLink} to={`/admin/books/${book.id}`}>
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

      <ButtonGroup spacing={5}>
        {request.status === "Waiting" && (
          <>
            <FormButton
              path={`/admin/borrow-requests/${request.id}/approve`}
              method="post"
              label="Approve"
              text="Approve"
              colorScheme="green"
              hasValue
              name="isApproved"
              value={true}
              onSubmit={(event) => {
                if (
                  !confirm("Please confirm you want to APPROVE this request.")
                ) {
                  event.preventDefault();
                }
              }}
              mt={4}
              p={5}
            />
            <FormButton
              path={`/admin/borrow-requests/${request.id}/approve`}
              method="post"
              label="Reject"
              text="Reject"
              colorScheme="red"
              hasValue
              name="isApproved"
              value={false}
              onSubmit={(event) => {
                if (
                  !confirm("Please confirm you want to REJECT this request.")
                ) {
                  event.preventDefault();
                }
              }}
              mt={4}
              p={5}
            />
          </>
        )}
        <LinkButton
          path="/admin/borrow-requests"
          text="Back To List"
          label="Back To List"
          mt={4}
          p={5}
          variant="outline"
          colorScheme="teal"
        />
      </ButtonGroup>
    </VStack>
  );
}

import { useContext } from "react";
import { BorrowRequestContext } from "../../Contexts/BorrowRequestContext";
import { getBookById } from "../../Apis/BookApis";
import { useEffect } from "react";
import { useState } from "react";
import {
  Box,
  ButtonGroup,
  Heading,
  Image,
  Link,
  LinkBox,
  VStack,
  Wrap,
  WrapItem,
} from "@chakra-ui/react";
import { NavLink, redirect } from "react-router-dom";
import { FormButton } from "../../Components/FormIconButton/FormButton";
import { createBorrowRequest } from "../../Apis/BorrowRequestApis";
import { LinkButton } from "../../Components";

export async function action({ request }) {
  const formData = await request.formData();
  const requestModel = Object.fromEntries(formData);

  requestModel.bookIds = requestModel.bookIds.split(",");

  console.log(requestModel.bookIds);

  await createBorrowRequest(requestModel);

  return redirect("/borrow-books");
}

export function BorrowBookNewPage() {
  const requestContext = useContext(BorrowRequestContext);
  const bookIds = requestContext.borrowRequest;

  const [requestBooks, setRequestBook] = useState([]);

  useEffect(() => {
    async function fetchData() {
      await Promise.all(bookIds.map((id) => getBookById(id))).then((values) => {
        setRequestBook(values);
      });
    }
    fetchData();
  }, [bookIds]);

  const noContent = (
    <Heading size="lg" textAlign="center">
      EMPTY REQUEST!{" "}
      <Link as={NavLink} to={"/books"} color="teal">
        GET SOME BOOKS HERE
      </Link>
    </Heading>
  );

  const sendRequestForm = (
    <VStack w="full" align="left" spacing={10}>
      <Heading>Check your request</Heading>
      <Wrap spacing={10}>
        {requestBooks.map((book) => (
          <WrapItem key={book.id} w="15%">
            <VStack w="full" spacing={2} p={0} align="center">
              <LinkBox as={NavLink} to={`/books/${book.id}`}>
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
                  mt={10}
                >{`#${book.id} ${book.name}`}</Heading>
              </LinkBox>
              <Box
                onClick={() => {
                  requestContext.removeBookFromRequest(book.id);
                }}
              >
                <Link color="red">Remove</Link>
              </Box>
            </VStack>
          </WrapItem>
        ))}
      </Wrap>
      <ButtonGroup spacing={5}>
        <FormButton
          path={"/borrow-books/new"}
          method="post"
          label="Send Request"
          text="Send Request"
          colorScheme="teal"
          hasValue
          name="bookIds"
          value={bookIds}
          onSubmit={(event) => {
            if (!confirm("Please confirm you want to SEND this request.")) {
              event.preventDefault();
            }
            requestContext.clearRequest();
          }}
          mt={4}
          p={5}
        />
        <LinkButton
          path="/books"
          text="Get Another Book"
          label="Get Another Book"
          mt={4}
          p={5}
          colorScheme="red"
        />
      </ButtonGroup>
    </VStack>
  );

  return (
    <Box w="full" p={0}>
      {requestBooks.length > 0 ? sendRequestForm : noContent}
    </Box>
  );
}

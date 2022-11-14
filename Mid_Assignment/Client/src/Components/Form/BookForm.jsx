import { Center, Heading, VStack, Button, ButtonGroup } from "@chakra-ui/react";
import { useState } from "react";
import { Form, useNavigate } from "react-router-dom";
import {
  minLengthValidate,
  requiredValidate,
} from "../../Helpers/InputValidations";
import { CustomCheckboxGroup } from "./CustomCheckboxGroup";
import { TextAreaInput } from "./TextAreaIntput";
import { TextInput } from "./TextInput";

export function BookForm(props) {
  const { path, method, title, data, allCategories, ...otherProps } = props;

  const [book, setBook] = useState(data);

  const navigate = useNavigate();

  return (
    <Form action={path} method={method}>
      <Center>
        <VStack w="60%" p={10} spacing={5}>
          <Heading>{title}</Heading>
          <TextInput
            label="Book Name"
            name="name"
            type="text"
            isRequired
            defaultValue={book.name}
            handleChange={(value) => {
              setBook({ ...book, ["name"]: value });
            }}
            validateInput={(input) => {
              let errorMessage =
                requiredValidate(input) || minLengthValidate(input, 2);

              let isError = errorMessage !== "";

              return { isError, errorMessage };
            }}
          />
          <TextInput
            label="Cover URL"
            name="cover"
            type="text"
            isRequired={false}
            defaultValue={book.cover}
            handleChange={(value) => {
              setBook({ ...book, ["cover"]: value });
            }}
            validateInput={(input) => ({ isError: false, errorMessage: "" })}
          />
          <TextAreaInput
            label="Description"
            name="description"
            isRequired={false}
            defaultValue={book.description}
            handleChange={(value) => {
              setBook({ ...book, ["description"]: value });
            }}
            validateInput={(input) => ({ isError: false, errorMessage: "" })}
          />
          <CustomCheckboxGroup
            data={[...allCategories]}
            label="Category"
            name="categoryIds"
            defaultValue={book.categories ?? []}
            handleCheckbox={(value) => {
              setBook({ ...book, ["categoryIds"]: value });
            }}
            validateInput={(input) => ({ isError: false, errorMessage: "" })}
          />
          <ButtonGroup>
            <Button
              mt={4}
              p={5}
              colorScheme="red"
              onClick={() => {
                navigate(-1);
              }}
            >
              Cancel
            </Button>
            <Button mt={4} p={5} type="submit" colorScheme="teal">
              Submit
            </Button>
          </ButtonGroup>
        </VStack>
      </Center>
    </Form>
  );
}

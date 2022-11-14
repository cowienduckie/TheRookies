import {
  Checkbox,
  CheckboxGroup,
  FormControl,
  FormLabel,
  FormHelperText,
  FormErrorMessage,
  Wrap,
  Input,
} from "@chakra-ui/react";
import { useState } from "react";

export function CustomCheckboxGroup(props) {
  const {
    data,
    label,
    name,
    defaultValue,
    handleCheckbox,
    validateInput,
    helperText,
  } = props;

  const [input, setInput] = useState(defaultValue.map((v) => "" + v.id));

  const handleChange = (value) => {
    handleCheckbox(value);
    setInput(value);
  };

  const { isError, errorMessage } = validateInput(input);

  return (
    <>
      <FormControl isInvalid={isError}>
        <FormLabel>{label}</FormLabel>
        <Input name={name} value={input} readOnly hidden />
        {!isError ? (
          <FormHelperText>{helperText}</FormHelperText>
        ) : (
          <FormErrorMessage>{errorMessage}</FormErrorMessage>
        )}
      </FormControl>

      <CheckboxGroup value={input} colorScheme="teal" onChange={handleChange}>
        <Wrap spacing={5}>
          {data.map((category) => (
            <Checkbox key={category.id} value={"" + category.id}>
              {category.name}
            </Checkbox>
          ))}
        </Wrap>
      </CheckboxGroup>
    </>
  );
}

import {
  FormControl,
  FormLabel,
  FormHelperText,
  FormErrorMessage,
  Textarea,
} from "@chakra-ui/react";
import { useState } from "react";

export function TextAreaInput(props) {
  const {
    label,
    name,
    defaultValue,
    isRequired,
    handleChange,
    validateInput,
    helperText
  } = props;

  const [input, setInput] = useState(defaultValue);

  const handleInputChange = (event) => {
    handleChange(event.target.value);
    setInput(event.target.value);
  };

  const { isError, errorMessage } = validateInput(input);

  return (
    <FormControl isInvalid={isError} isRequired={isRequired}>
      <FormLabel>{label}</FormLabel>
      <Textarea value={input} name={name} onChange={handleInputChange} />
      {!isError ? (
        <FormHelperText>{helperText}</FormHelperText>
      ) : (
        <FormErrorMessage>{errorMessage}</FormErrorMessage>
      )}
    </FormControl>
  );
}

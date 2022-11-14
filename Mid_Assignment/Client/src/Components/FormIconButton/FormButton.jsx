import { Button, Input } from "@chakra-ui/react";
import { Form } from "react-router-dom";

export function FormButton(props) {
  const {
    path,
    method,
    onSubmit,
    label,
    colorScheme,
    text,
    hasValue = false,
    name,
    value,
    ...otherProps
  } = props;

  return (
    <Form action={path} method={method} onSubmit={(event) => onSubmit(event)}>
      <Button
        type="submit"
        aria-label={label}
        colorScheme={colorScheme}
        {...otherProps}
      >
        {text}
      </Button>
      {hasValue && <Input name={name} value={value} readOnly hidden />}
    </Form>
  );
}

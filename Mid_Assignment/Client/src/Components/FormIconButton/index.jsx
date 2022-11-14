import { Icon, IconButton, Input } from "@chakra-ui/react";
import { Form } from "react-router-dom";

export function FormIconButton(props) {
  const {
    path,
    method,
    onSubmit,
    label,
    colorScheme,
    icon,
    hasValue = false,
    name,
    value
  } = props;

  return (
    <Form action={path} method={method} onSubmit={(event) => onSubmit(event)}>
      <IconButton
        type="submit"
        aria-label={label}
        colorScheme={colorScheme}
        icon={<Icon as={icon} />}
      />
      {hasValue && <Input name={name} value={value} readOnly hidden />}
    </Form>
  );
}

import { Icon, IconButton, LinkBox } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";

export function LinkIconButton(props) {
  const { path, label, colorScheme, icon, ...otherProps } = props;

  return (
    <LinkBox as={NavLink} to={path}>
      <IconButton
        aria-label={label}
        colorScheme={colorScheme}
        icon={<Icon as={icon} />}
        {...otherProps} />
    </LinkBox>
  );
}
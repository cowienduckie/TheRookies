import { ViewIcon, DeleteIcon, EditIcon } from "@chakra-ui/icons";
import { ButtonGroup } from "@chakra-ui/react";
import { FormIconButton } from "../form-button/FormIconButton";
import { LinkIconButton } from "../link-button/LinkIconButton";

export function TableAction(props) {
  const { objectId, ...otherProps } = props;

  return (
    <ButtonGroup>
      <LinkIconButton
        path={`/rookies/${objectId}`}
        label='Details'
        colorScheme='blue'
        icon={ViewIcon} />
      <LinkIconButton
        path={``}
        label='Edit'
        colorScheme='teal'
        icon={EditIcon} />
      <FormIconButton
        path={`/rookies/${objectId}/delete`}
        method='post'
        label='Delete'
        colorScheme='red'
        icon={DeleteIcon}
        onSubmit={(event) => {
          if (
            !confirm(
              "Please confirm you want to delete this record."
            )
          ) {
            event.preventDefault();
          }
        }} />
    </ButtonGroup>
  )
}
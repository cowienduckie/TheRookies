import { ViewIcon, DeleteIcon, EditIcon } from "@chakra-ui/icons";
import { ButtonGroup } from "@chakra-ui/react";
import { FormIconButton } from "../FormIconButton";
import { LinkIconButton } from "../LinkButton/LinkIconButton";

export function TableAction(props) {
  const { objectId, resourcePath, hasDetailView = true, ...otherProps } = props;

  return (
    <ButtonGroup>
      {hasDetailView &&
        <LinkIconButton
          path={`${resourcePath}/${objectId}`}
          label='Details'
          colorScheme='blue'
          icon={ViewIcon}
        />
      }
      <LinkIconButton
        path={`${resourcePath}/${objectId}/edit`}
        label='Edit'
        colorScheme='teal'
        icon={EditIcon}
      />
      <FormIconButton
        path={`${resourcePath}/${objectId}/delete`}
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
        }}
      />
    </ButtonGroup>
  )
}
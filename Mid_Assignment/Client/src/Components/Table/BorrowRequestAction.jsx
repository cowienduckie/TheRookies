import {
  ViewIcon,
  DeleteIcon,
  EditIcon,
  CheckIcon,
  CloseIcon,
} from "@chakra-ui/icons";
import { ButtonGroup } from "@chakra-ui/react";
import { FormIconButton } from "../FormIconButton";
import { LinkIconButton } from "../LinkButton/LinkIconButton";

export function BorrowRequestAction(props) {
  const {
    objectId,
    resourcePath,
    isAdmin = false,
    isWaiting = false,
    ...otherProps
  } = props;

  return (
    <ButtonGroup>
      <LinkIconButton
        path={`${resourcePath}/${objectId}`}
        label="Details"
        colorScheme="blue"
        icon={ViewIcon}
      />
      {isAdmin && isWaiting && (
        <>
          <FormIconButton
            path={`${resourcePath}/${objectId}/approve`}
            method="post"
            label="Approve"
            colorScheme="green"
            icon={CheckIcon}
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
          />
          <FormIconButton
            path={`${resourcePath}/${objectId}/approve`}
            method="post"
            label="Reject"
            colorScheme="red"
            icon={CloseIcon}
            hasValue
            name="isApproved"
            value={false}
            onSubmit={(event) => {
              if (!confirm("Please confirm you want to REJECT this request.")) {
                event.preventDefault();
              }
            }}
          />
        </>
      )}
    </ButtonGroup>
  );
}

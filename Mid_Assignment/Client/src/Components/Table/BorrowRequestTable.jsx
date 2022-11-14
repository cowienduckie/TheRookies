import {
  Table,
  TableContainer,
  Tbody,
  Thead,
  Tr,
  Th,
  Td,
} from "@chakra-ui/react";
import { BorrowRequestAction } from "./BorrowRequestAction";

export function BorrowRequestTable(props) {
  const {
    data = [],
    resourcePath,
    hasIndex = false,
    hasAction = false,
    isAdmin = false,
    ...otherProps
  } = props;

  const headers = [
    "Id",
    "Status",
    "Requested At",
    "Requested By",
    "Approved At",
    "Approve By",
  ];

  return (
    <TableContainer>
      <Table variant="simple">
        <Thead>
          <Tr>
            {hasIndex && <Th>#</Th>}
            {headers && headers.map((header) => <Th key={header}>{header}</Th>)}
            {hasAction && <Th>Actions</Th>}
          </Tr>
        </Thead>
        <Tbody>
          {data.map((value, index) => (
            <Tr key={index}>
              {hasIndex && <Td>{index + 1}</Td>}
              <Td>{value.id}</Td>
              <Td>{value.status}</Td>
              <Td>{value.requestedAt}</Td>
              <Td>{value.requester.name}</Td>
              <Td>{value.approvedAt}</Td>
              <Td>{value.approver && `${value.approver.name}`}</Td>
              {hasAction && (
                <Td>
                  <BorrowRequestAction
                    objectId={value.id}
                    resourcePath={resourcePath}
                    isAdmin={isAdmin}
                    isWaiting={value.status == "Waiting"}
                  />
                </Td>
              )}
            </Tr>
          ))}
        </Tbody>
      </Table>
    </TableContainer>
  );
}

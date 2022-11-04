import { HStack, VStack, Heading, Text, Image } from "@chakra-ui/react";
import { useLoaderData } from "react-router-dom";
import { getProfile } from "../../apis/rookies";

export async function loader({ params }) {
  const profile = await getProfile();

  if (!profile) {
    throw new Response("", {
      status: 404,
      statusText: 'Not Found'
    })
  }

  return profile;
}

export function ProfilePage() {
  const profile = useLoaderData();

  return <>
    <HStack w='full' spacing={10} p={0} mt={20} align='top'>
      <Image
        borderRadius='full'
        boxSize='30%'
        src='https://dummyimage.com/300x300/'
        alt='Dummy image'
        m='auto' />
      <VStack w='70%' p={10} spacing={10} bgColor='gray.50' align='left'>
        <Heading size='2xl'>USER PROFILE</Heading>
        <Text fontSize='2xl'><strong>ID:</strong> {profile.id}</Text>
        <Text fontSize='2xl'><strong>Username:</strong> {profile.username}</Text>     
      </VStack>
    </HStack>
  </>
}
export default interface Apartment {
    id: string;
    externalId: string | null;
    hostId: string;
    title: string;
    description: string;
    address: string;
    pricePerNight: number;
    isAvailable: boolean;
    createdAt: string;
    updatedAt: string | null;
    deletedAt: string | null;
}
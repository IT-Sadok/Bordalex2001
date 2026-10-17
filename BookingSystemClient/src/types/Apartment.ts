export default interface Apartment {
    id: string;
    externalId: string | null;
    hostId: string;
    title: string;
    description: string;
    address: string;
    pricePerNight: number;
    primaryImageUrl: string | null;
    isAvailable: boolean;
    createdAt: string;
    updatedAt: string | null;
    deletedAt: string | null;
}
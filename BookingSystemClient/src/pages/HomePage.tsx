import { useCallback, useEffect, useState } from "react";
import ApartmentGrid from "../components/Apartments/ApartmentGrid/ApartmentGrid";
import { getAllAvailableApartmentsAsync } from "../services/apartmentService";
import type Apartment from "../types/Apartment";
import type ApartmentCardProps from "../components/Apartments/ApartmentCard/ApartmentCardProps";
import getImageUrl from "../utils/getImageUrl";
import type PagedResult from "../types/PagedResult";

const apartmentCards = (response: PagedResult<Apartment>) =>
  response.items.map((apartment) => ({
    id: apartment.id,
    title: apartment.title,
    location: apartment.address,
    pricePerNight: apartment.pricePerNight,
    imageUrl: getImageUrl(apartment.primaryImageUrl),
  }));

export default function HomePage() {
  const [apartments, setApartments] = useState<ApartmentCardProps[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);

  const pageSize = 4;

  const loadApartmentsAsync = useCallback(async () => {
    setIsLoading(true);
    setError(null);

    try {
      const result = await getAllAvailableApartmentsAsync(
        currentPage,
        pageSize,
      );

      setApartments(apartmentCards(result));
      setTotalPages(result.totalPages);
    } catch (error) {
      console.error("Error loading apartments:", error);
      setError("Failed to load apartments. Please try again later.");
    } finally {
      setIsLoading(false);
    }
  }, [currentPage]);

  useEffect(() => {
    void loadApartmentsAsync();
  }, [loadApartmentsAsync]);

  return (
    <>
      <div className="mx-auto w-full px-4 py-8 sm:px-6 lg:px-8">
        <section>
          <div className="mb-6 flex items-end justify-between gap-4">
            <div>
              <h1 className="text-2xl font-semibold">Explore apartments</h1>
              <p className="mt-1 text-sm text-gray-500">
                Find a place for your next stay
              </p>
            </div>

            <button
              type="button"
              className="flex shrink-0 items-center gap-2 rounded-xl border border-gray-300 px-4 py-2 text-sm font-medium transition-colors hover:bg-gray-100"
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 16 16"
                fill="currentColor"
                className="size-4"
                aria-hidden="true"
              >
                <path d="M2.75 4.5h6.69a2.25 2.25 0 1 0 0-1.5H2.75a.75.75 0 0 0 0 1.5ZM13.25 3a.75.75 0 1 1 0 1.5.75.75 0 0 1 0-1.5ZM6.56 9.5H2.75a.75.75 0 0 0 0 1.5h3.81a2.25 2.25 0 1 0 0-1.5Zm3.69.75a.75.75 0 1 1 1.5 0 .75.75 0 0 1-1.5 0Z" />
              </svg>
              Filters
            </button>
          </div>

          <ApartmentGrid
            apartments={apartments}
            isLoading={isLoading}
            error={error}
            onRetry={() => void loadApartmentsAsync()}
          />

          {!isLoading && !error && totalPages > 1 && (
            <div className="mt-8 flex items-center justify-center gap-4">
              <button
                type="button"
                disabled={currentPage === 1}
                onClick={() => setCurrentPage((page) => page - 1)}
                className="rounded-lg border px-4 py-2 cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed"
              >
                Previous
              </button>

              <span>
                Page {currentPage} of {totalPages}
              </span>

              <button
                type="button"
                disabled={currentPage >= totalPages}
                onClick={() => setCurrentPage((page) => page + 1)}
                className="rounded-lg border px-4 py-2 cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed"
              >
                Next
              </button>
            </div>
          )}
        </section>
      </div>
    </>
  );
}

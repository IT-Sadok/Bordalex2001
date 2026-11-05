import { useCallback, useEffect, useMemo, useState } from "react";
import ApartmentGrid from "../components/Apartments/ApartmentGrid/ApartmentGrid";
import { getAllAvailableApartmentsAsync } from "../services/apartmentService";
import type Apartment from "../types/Apartment";
import type ApartmentCardProps from "../components/Apartments/ApartmentCard/ApartmentCardProps";
import getImageUrl from "../utils/getImageUrl";
import type PagedResult from "../types/PagedResult";
import Pagination from "../components/Apartments/Pagination/Pagination";
import type ApartmentSortOption from "../types/ApartmentSortOption";
import ApartmentSort from "../components/Apartments/ApartmentSort/ApartmentSort";
import { useSearchParams } from "react-router";
import parseApartmentSearchParams from "../utils/parseApartmentSearchParams";

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
  const [retryCount, setRetryCount] = useState(0);
  const [sortOption, setSortOption] = useState<ApartmentSortOption>("newest");

  const pageSize = 4;

  const sortBy = sortOption.startsWith("price") ? "price" : "date";

  const sort =
    sortOption === "newest" || sortOption === "price-desc" ? "desc" : "asc";

  const [searchParams] = useSearchParams();

  const search = searchParams.toString();

  const apartmentSearchParams = useMemo(
    () => parseApartmentSearchParams(new URLSearchParams(search)),
    [search],
  );

  const loadApartmentsAsync = useCallback(
    async (signal?: AbortSignal) => {
      setIsLoading(true);
      setError(null);

      try {
        const result = await getAllAvailableApartmentsAsync(
          {
            page: currentPage,
            size: pageSize,
            sort,
            sortBy,
            destination: apartmentSearchParams?.destination || undefined,
          },
          signal,
        );

        if (signal?.aborted) return;

        setApartments(apartmentCards(result));
        setTotalPages(result.totalPages);
      } catch (error) {
        if (signal?.aborted) return;

        console.error("Error loading apartments:", error);
        setError("Failed to load apartments. Please try again later.");
      } finally {
        if (!signal?.aborted) {
          setIsLoading(false);
        }
      }
    },
    [currentPage, retryCount, sort, sortBy, apartmentSearchParams?.destination],
  );

  useEffect(() => {
    const controller = new AbortController();

    void loadApartmentsAsync(controller.signal);

    return () => {
      controller.abort();
    };
  }, [loadApartmentsAsync]);

  return (
    <>
      <div className="mx-auto w-full px-4 py-8 sm:px-6 lg:px-8">
        <section>
          <div className="mb-6 flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
            <div>
              <h1 className="text-2xl font-semibold">Explore apartments</h1>
              <p className="mt-1 text-sm text-gray-500">
                Find a place for your next stay
              </p>
            </div>

            <div className="flex flex-wrap items-center gap-3">
              <ApartmentSort
                value={sortOption}
                onChange={(value) => {
                  setCurrentPage(1);
                  setSortOption(value);
                }}
              />

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
          </div>

          <ApartmentGrid
            apartments={apartments}
            isLoading={isLoading}
            error={error}
            onRetry={() => setRetryCount((count) => count + 1)}
          />

          {!isLoading && !error && (
            <Pagination
              currentPage={currentPage}
              totalPages={totalPages}
              onPageChange={setCurrentPage}
            />
          )}
        </section>
      </div>
    </>
  );
}

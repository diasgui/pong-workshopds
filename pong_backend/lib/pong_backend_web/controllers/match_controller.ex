defmodule PongBackendWeb.MatchController do
  use PongBackendWeb, :controller

  alias PongBackend.Accounts

  action_fallback PongBackendWeb.FallbackController

  def find_match(conn, %{"url" => url}) do
    case Redix.pipeline(RedixConnection, [["MULTI"], ["GET", "match_url"], ["DEL", "match_url"], ["EXEC"]]) do
      {:ok, [_, _, _, [nil, _]]} ->
        {:ok, _} = Redix.command(RedixConnection, ["SET", "match_url", url])
        render(conn, "waiting.json")
      {:ok, [_, _, _, [match_url, _]]} ->
        render(conn, "found.json", match_url: match_url)
    end
  end

  def match_end(conn, %{"winner_id" => winner_id, "loser_id" => loser_id}) do
    with {1, [wins]} <- Accounts.increment_wins(winner_id),
      {1, [_]} <- Accounts.increment_losses(loser_id),
      {:ok, _} <- Redix.command(RedixConnection, ["ZADD", "leaderboard", wins, winner_id]) do
      render(conn, "success.json")
    end
  end
end

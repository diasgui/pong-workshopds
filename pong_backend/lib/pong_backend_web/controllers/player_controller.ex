defmodule PongBackendWeb.PlayerController do
  use PongBackendWeb, :controller

  alias PongBackend.Accounts
  alias PongBackend.Accounts.Player

  action_fallback PongBackendWeb.FallbackController

  def index(conn, _params) do
    players = Accounts.list_players()
    render(conn, "index.json", players: players)
  end

  def create(conn, %{"player" => player_params}) do
    with {:ok, %Player{} = player} <- Accounts.create_player(player_params) do
      conn
      |> put_status(:created)
      |> put_resp_header("location", Routes.player_path(conn, :show, player))
      |> render("show.json", player: player)
    end
  end

  def show(conn, %{"id" => id}) do
    player = Accounts.get_player!(id)
    render(conn, "show.json", player: player)
  end

  def update(conn, %{"id" => id, "player" => player_params}) do
    player = Accounts.get_player!(id)

    with {:ok, %Player{} = player} <- Accounts.update_player(player, player_params) do
      render(conn, "show.json", player: player)
    end
  end

  def delete(conn, %{"id" => id}) do
    player = Accounts.get_player!(id)

    with {:ok, %Player{}} <- Accounts.delete_player(player) do
      send_resp(conn, :no_content, "")
    end
  end

  def change_name(conn, %{"name" => name}) do
    player = Accounts.get_player!(conn.private.guardian_default_resource.id)

    with {:ok, %Player{} = player} <- Accounts.update_player(player, %{"name" => name}) do
      render(conn, "show.json", player: player)
    end
  end

  def leaderboard(conn, _params) do
    with {:ok, leaderboard} <- Redix.command(RedixConnection, ["ZREVRANGEBYSCORE", "leaderboard", "+inf", "-inf"]) do
      players = for player_id <- leaderboard, do: Accounts.get_player!(player_id)
      render(conn, "index.json", players: players)
    end
  end
end

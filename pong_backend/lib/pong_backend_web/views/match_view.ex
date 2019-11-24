defmodule PongBackendWeb.MatchView do
  use PongBackendWeb, :view
  alias PongBackendWeb.MatchView

  def render("waiting.json", %{}) do
    %{
      status: :waiting
    }
  end

  def render("found.json", %{match_url: match_url}) do
    %{
      status: :found,
      match_url: match_url
    }
  end

  def render("success.json", %{}) do
    %{
      status: :ok
    }
  end
end
